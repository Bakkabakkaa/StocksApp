

https://github.com/user-attachments/assets/46dc1651-0aab-4165-b978-623a41c8af47




Это учебное веб-приложение, в котором реализована базовая логика торговли акциями.

В приложении можно искать акции, получать информацию по ним и создавать ордера на покупку и продажу. Также есть отображение истории операций и данных на dashboard.

Серверная часть написана на ASP.NET Core с использованием REST API. Для работы с базой данных использовал Entity Framework Core. Данные по акциям получаются через внешний API (Finnhub).

Для работы требуется API-ключ Finnhub.

Перед запуском выполните команды:

dotnet user-secrets init --project StockMarketSolution  
dotnet user-secrets set "FinnhubToken" "YOUR_API_KEY" --project StockMarketSolution

API-ключ можно получить на сайте https://finnhub.io/

