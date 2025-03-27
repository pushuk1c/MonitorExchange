using System.Xml.Linq;
using System.Xml.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonitorExchange.Dtos.FEImport;
using MonitorExchange.Dtos.FEOffers;
using MonitorExchange.Models;
using MonitorExchange.Services.FileExchangeService;
using MonitorExchange.Services.GoodsService;
using MonitorExchange.Services.GoodsSizeService;
using MonitorExchange.Services.StockService;

public class UploadFilesExchange : BackgroundService
{
    private readonly ILogger<UploadFilesExchange> _logger;

    private readonly IServiceScopeFactory _scopeFactory;
       
    private readonly string _watchPath = @"FilesStorage";

    public UploadFilesExchange(ILogger<UploadFilesExchange> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("UploadFilesExchange run.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope()) 
                {
                    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                    var fileExchangeService = scope.ServiceProvider.GetRequiredService<IFileExchangeService>();
                    var goodsService = scope.ServiceProvider.GetRequiredService<IGoodsService>();
                    var goodsSizeService = scope.ServiceProvider.GetRequiredService<IGoodsSizeService>();
                    var stockService = scope.ServiceProvider.GetRequiredService<IStockService>();

                    await UploadPackagesAsync(mapper, fileExchangeService, goodsService, goodsSizeService, stockService);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing files.");
            }

            await Task.Delay(15000, stoppingToken);
        }

        _logger.LogInformation("UploadFilesExchange stopped.");
    }

    private async Task UploadPackagesAsync(IMapper mapper, IFileExchangeService fileExchangeService, IGoodsService goodsService, IGoodsSizeService goodsSizeService, IStockService stockService)
    {
        List<Package> listPackages = new List<Package>();

        await AssemblePackagesAsync(listPackages);
                
        var collectedPackagesImports = listPackages.Where(p => p.Collected && p.Type == "import").ToList();
        foreach (var package in collectedPackagesImports)
        {
            await UploadPackageImportAsync(package, mapper, fileExchangeService, goodsService, goodsSizeService);
        }

        var collectedPackagesOffers = listPackages.Where(p => p.Collected && p.Type == "offers").ToList();
        foreach (var package in collectedPackagesOffers)
        {
            await UploadPackageOffersAsync(package, mapper, fileExchangeService, goodsService, goodsSizeService, stockService);
        }

    }

    private async Task UploadPackageImportAsync(Package package, IMapper mapper, IFileExchangeService fileExchangeService, IGoodsService goodsService, IGoodsSizeService goodsSizeService)
    {
        List<FEImport> imports = new List<FEImport>();

        package.Items.Sort();

        foreach (var item in package.Items)
        {
            FileExchange fileExchange = new FileExchange
            {
                StrId = package.StrId,
                Item = item,
                InAll = package.InAll,
                Type = package.Type,
                DataCreate = DateTime.Now,
            };

            try  // File loading and parsing & Data save to base
            {
                var patch = $"{_watchPath}\\{package.Type}_{item}_{package.InAll}_{package.StrId}.xml";

                XElement xml = XElement.Load(patch);

                ParsingXmlDataImport(xml, out AddFEImportXMLDto FEImport);

                var goodses = FEImport.Catalog.Goodses.Select(g => mapper.Map<Goods>(g)).ToList();

                var goodsSizes = FEImport.Catalog.Goodses.Select(gs =>
                                        new GoodsSize
                                        {
                                            strId = gs.GoodsSize.strId,
                                            Name = gs.GoodsSize.Name,
                                            Goods = goodses.FirstOrDefault(g => g.strId == gs.GoodsSize.GoodsId),
                                        }).ToList();

                var _fileExchange = await fileExchangeService.SaveFileExchangeToBaseAsync(fileExchange);
                if (_fileExchange is null)
                    throw new Exception("Error save File Exchange!");

                var _goodses = await goodsService.SaveGoodsesToBaseAsync(goodses);
                if (_goodses is null)
                    throw new Exception("Error save Goodses!");

                var _goodsSizes = await goodsSizeService.SaveGoodsSizesToBaseAsync(goodsSizes);
                if (_goodsSizes is null)
                    throw new Exception("Error save Goods Sizes!");

                foreach (var goods in _goodses)
                {
                    var __goodsSizes = _goodsSizes.Where(g => g.Goods.Equals(goods)).Select(g => g).ToList();
                    foreach (var goodsSize in __goodsSizes)
                    {
                        imports.Add(new FEImport
                        {
                            DateTime = DateTime.Now,
                            FileExchange = _fileExchange,
                            Goods = goods,
                            GoodsSize = goodsSize,
                        });
                    }

                }

                var _imports = await fileExchangeService.SaveFileExchangeImportToBaseAsync(imports);
                if (_imports is null)
                    throw new Exception("Error save Import!");

                fileExchange.IsUpload = true;
                var _updateFileExchange = await fileExchangeService.UpdateFileExchangeToBaseAsync(fileExchange);
                if (_updateFileExchange is null)
                    throw new Exception("Error update File Exchange!");
                
                if (File.Exists(patch))
                    File.Delete(patch); 

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"UploadFilesExchange: {ex.Message}");
            }

        }

    }

    private async Task UploadPackageOffersAsync(Package package, IMapper mapper, IFileExchangeService fileExchangeService, IGoodsService goodsService, IGoodsSizeService goodsSizeService, IStockService stockService)
    {
        List<FEOffers> offers = new List<FEOffers>();

        package.Items.Sort();

        foreach (var item in package.Items)
        {
            FileExchange fileExchange = new FileExchange
            {
                StrId = package.StrId,
                Item = item,
                InAll = package.InAll,
                Type = package.Type,
                DataCreate = DateTime.Now,
            };

            try  // File loading and parsing & Data save to base
            {
                var patch = $"{_watchPath}\\{package.Type}_{item}_{package.InAll}_{package.StrId}.xml";

                XElement xml = XElement.Load(patch);

                ParsingXmlDataOffers(xml, out AddFEOffersXMLDto FEOffers);
                     
                var newIdsGoods = FEOffers.Packet.Offers.Select(o => o.strId.Trim().Substring(0, 36)).Distinct().ToList();
                var newIdsGoodsSize = FEOffers.Packet.Offers.Select(o => o.strId.Trim().Substring(37, 36)).ToList();

                var _goods = await goodsService.GetGoodsesByIds(newIdsGoods);
                if (_goods is null || !_goods.Any())
                    throw new Exception("Error !");

                var _goodSizes = await goodsSizeService.GetGoodsSizesByIds(newIdsGoods,newIdsGoodsSize);
                if (_goodSizes is null || !_goodSizes.Any())
                    throw new Exception("Error !");

                var _fileExchange = await fileExchangeService.SaveFileExchangeToBaseAsync(fileExchange);
                if (_fileExchange is null)
                    throw new Exception("Error !");

                var _stocks = await stockService.SaveStocksToBaseAsync(FEOffers.Packet.Stocks.Select(s => mapper.Map<Stock>(s)).ToList());
                if (_stocks is null || !_stocks.Any())
                    throw new Exception("Error !");

                foreach (var offer in FEOffers.Packet.Offers)
                {
                    var _price = 0m;
                    var _discountedPrice = 0m;

                    if (offer.Prices.Count > 0)
                    {
                        _price = offer.Prices[0].standartPrice;
                        _discountedPrice = offer.Prices[0].discountedPrice;
                    }

                    foreach (var stock in offer.OfferStocks)
                    {
                        offers.Add(new FEOffers
                        {
                            DateTime = DateTime.Now,
                            FileExchange = _fileExchange,
                            Goods = _goods.FirstOrDefault(g => g.strId == offer.strId.Trim().Substring(0, 36)),
                            GoodsSize = _goodSizes.FirstOrDefault(gs => gs.strId == offer.strId.Trim().Substring(37, 36)),
                            Stock = _stocks.FirstOrDefault(s => s.StrId == stock.strIdStock),
                            price = _price,
                            discountedPrice = _discountedPrice,
                            quantity = stock.Quantity
                        });
                    }
                }
               
                var _offers = await fileExchangeService.SaveFileExchangeOffersToBaseAsync(offers);
                if (_offers is null || !_offers.Any())
                    throw new Exception("Error !");

                fileExchange.IsUpload = true;
                var _updateFileExchange = await fileExchangeService.UpdateFileExchangeToBaseAsync(fileExchange);
                if (_updateFileExchange is null)
                    throw new Exception("Error update File Exchange!");

                if (File.Exists(patch))
                    File.Delete(patch);

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"UploadFilesExchange: {ex.Message}");
            }

        }

    }

    private async Task AssemblePackagesAsync(List<Package> listPackages)
    {
        var files = Directory.GetFiles(_watchPath, "*.xml");

        foreach (var file in files)
        {
            //_logger.LogInformation($"Processing the file: {file}");

            var fileName = Path.GetFileName(file);
            string[] arrayParameters = fileName.Replace(".xml", "").Split("_");

            if (arrayParameters.Length != 4)
                continue;

            if (arrayParameters[3].Length != 36)
                continue;

            var Type = arrayParameters[0];
            var Item = arrayParameters[1];
            var InAll = arrayParameters[2];
            var StrId = arrayParameters[3];

            var package = listPackages.FirstOrDefault(p => p.StrId == StrId && p.Type == Type && p.InAll == int.Parse(InAll));
            if (package != null)
            {
                package.Items.Add(int.Parse(Item));
            }
            else
            {
                package = new Package { StrId = StrId, Type = Type, InAll = int.Parse(InAll), Items = new List<int> { int.Parse(Item) } };
                listPackages.Add(package);
            }

            package.CheckCollected();
        }
    }
    
    private void ParsingXmlDataImport(XElement? xml, out AddFEImportXMLDto FEImport)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AddFEImportXMLDto));

        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
        namespaces.Add("", "urn:1C.ru:commerceml_2");

        StringReader reader = new StringReader(xml.ToString());
        FEImport = (AddFEImportXMLDto)serializer.Deserialize(reader);
        FEImport.Init();
    }

    private void ParsingXmlDataOffers(XElement? xml, out AddFEOffersXMLDto FEOffers)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AddFEOffersXMLDto));

        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
        namespaces.Add("", "urn:1C.ru:commerceml_2");

        StringReader reader = new StringReader(xml.ToString());
        FEOffers = (AddFEOffersXMLDto)serializer.Deserialize(reader);
    }

    private class Package
    {
        public string StrId { get; set; }
        public string Type { get; set; }
        public int InAll { get; set; }
        public bool Collected { get; private set; }
        public List<int> Items { get; set; }

        public void CheckCollected()
        {
            var listItems = Items.Distinct().ToList();
            if (listItems.Count == InAll)
            {
                Collected = true;
            }
            else
            {
                Collected = false;
            }
               
        }

    }
}

