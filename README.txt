Set Multiple start up projects:
ChatJuanPabloPriotti Start
ChatJuanPabloPriotti.Receiver Start
● Allow registered users to log in and talk with other users in a chatroom. Covered
● Allow users to post messages as commands into the chatroom with the following format/stock=stock_code  Covered
● Create a decoupled bot that will call an API using the stock_code as a parameter(https://stooq.com/q/l/?s=aapl.us&f=sd2t2ohlcv&h&e=csv, here aapl.us is thestock_code)  Covered
● The bot should parse the received CSV file and then it should send a message back intothe chatroom using a message broker like RabbitMQ. The message will be a stock quoteusing the following format: “APPL.US quote is $93.42 per share”. The post owner will bethe bot. Covered (the decoupled message will appear in the console)
● Have the chat messages ordered by their timestamps and show only the last 50messages. Covered (not the part of the last 50)
● Unit test the functionality you prefer.  Covered
● Use .NET identity for users authentication Covered
