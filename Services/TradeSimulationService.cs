using Microsoft.AspNetCore.SignalR;
using _3rdEyeGitDemo.Hubs;
using _3rdEyeGitDemo.Models;

namespace _3rdEyeGitDemo.Services
{
    public class TradeSimulationService : BackgroundService
    {
        private readonly IHubContext<TradeHub> _hubContext;
        private readonly Random _random = new();
        private readonly Dictionary<string, TradeData> _currentPrices = new();

        public TradeSimulationService(IHubContext<TradeHub> hubContext)
        {
            _hubContext = hubContext;
            InitializeStocks();
        }

        private void InitializeStocks()
        {
            _currentPrices["AAPL"] = new TradeData 
            { 
                Symbol = "AAPL", 
                Price = 178.50m, 
                PreviousPrice = 178.50m,
                Volume = 50000000 
            };
            _currentPrices["MSFT"] = new TradeData 
            { 
                Symbol = "MSFT", 
                Price = 415.25m, 
                PreviousPrice = 415.25m,
                Volume = 25000000 
            };
            _currentPrices["GOOGL"] = new TradeData 
            { 
                Symbol = "GOOGL", 
                Price = 175.80m, 
                PreviousPrice = 175.80m,
                Volume = 15000000 
            };
            _currentPrices["META"] = new TradeData
            {
                Symbol = "META",
                Price = 300.80m,
                PreviousPrice = 175.80m,
                Volume = 5000000
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var symbol in _currentPrices.Keys.ToList())
                {
                    var trade = _currentPrices[symbol];
                    
                    // Store previous price
                    trade.PreviousPrice = trade.Price;
                    
                    // Simulate price change (between -2% and +2%)
                    var changePercent = (_random.NextDouble() - 0.5) * 4;
                    var priceChange = trade.Price * (decimal)(changePercent / 100);
                    trade.Price = Math.Round(trade.Price + priceChange, 2);
                    
                    // Calculate change metrics
                    trade.Change = Math.Round(trade.Price - trade.PreviousPrice, 2);
                    trade.ChangePercent = trade.PreviousPrice != 0 
                        ? Math.Round((trade.Change / trade.PreviousPrice) * 100, 2) 
                        : 0;
                    
                    // Determine price direction
                    if (trade.Change > 0)
                        trade.PriceDirection = "up";
                    else if (trade.Change < 0)
                        trade.PriceDirection = "down";
                    else
                        trade.PriceDirection = "unchanged";
                    
                    // Simulate volume change
                    var volumeChange = _random.Next(-1000000, 1000000);
                    trade.Volume = Math.Max(1000000, trade.Volume + volumeChange);
                    
                    trade.Timestamp = DateTime.UtcNow;
                    
                    await _hubContext.Clients.All.SendAsync("ReceiveTradeUpdate", trade, stoppingToken);
                }
                
                // Wait before next update (500ms to 2 seconds)
                var delay = _random.Next(500, 2000);
                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}