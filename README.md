# 3rdEyeGitDemo

A Blazor Server demo application showcasing real-time trade data streaming using SignalR and ag-Grid.

## Features

- Real-time stock price updates for Apple (AAPL), Microsoft (MSFT), and Google (GOOGL)
- SignalR/WebSocket communication for live data streaming
- ag-Grid for high-performance data display
- Visual price change indicators (green for up, red for down)
- Animated cell flashing on price updates
- Volume tracking with formatted display

## Technologies Used

- .NET 9.0
- Blazor Server with Interactive Server components
- SignalR for real-time communication
- ag-Grid Community Edition for data grid
- Bootstrap 5 for styling

## Getting Started

### Prerequisites

- .NET 9.0 SDK or later
- Visual Studio 2022 or VS Code

### Running the Application

1. Clone the repository
```bash
git clone https://github.com/saravanan3rdeye-art/3rdEyeGitDemo.git
cd 3rdEyeGitDemo
```

2. Restore dependencies
```bash
dotnet restore
```

3. Build the project
```bash
dotnet build
```

4. Run the application
```bash
dotnet run
```

5. Navigate to https://localhost:5001 or http://localhost:5000 in your browser

6. Click on "Trading Grid" in the navigation menu to view the real-time trading dashboard

## Project Structure

- `/Models` - Data models (TradeData)
- `/Hubs` - SignalR hub for real-time communication
- `/Services` - Background service for trade data simulation
- `/Components/Pages` - Blazor components including the TradingGrid
- `/wwwroot/js` - JavaScript for ag-Grid integration
- `/wwwroot/css` - Custom styling for the trading grid

## How It Works

1. The `TradeSimulationService` runs as a background service, generating random price updates for the three stocks
2. Updates are broadcast to all connected clients via the `TradeHub` SignalR hub
3. The `TradingGrid` component receives updates and displays them in an ag-Grid
4. JavaScript interop handles ag-Grid initialization and row updates with cell flashing animations
5. CSS classes provide visual feedback for price changes (green/red coloring)

## License

This is a demo project for educational purposes.