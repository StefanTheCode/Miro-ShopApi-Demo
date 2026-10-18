# ShopApi — demo .NET solution for the Miro video

A small **Clean Architecture** ASP.NET Core Web API with **EF Core (SQLite)**. This is the codebase you point the AI agent at on camera — intentionally small but realistic, so the generated architecture / ER / sequence diagrams look clean.

✅ Verified: builds with **.NET 8 SDK** and runs (`0 warnings, 0 errors`); the `Create Order` flow works end to end.

## Structure (the dependency rule)
```
ShopApi.sln
├── ShopApi.Domain          → entities: Customer, Product, Order, OrderItem, OrderStatus
├── ShopApi.Application      → abstractions (IOrderRepository, IProductRepository)
│                              + CreateOrder use case (command + handler)   [refs: Domain]
├── ShopApi.Infrastructure   → EF Core: AppDbContext, entity configurations,
│                              repositories, DI, DbSeeder                    [refs: Application, Domain]
└── ShopApi.Api              → controllers (Orders, Products) + Program.cs   [refs: Application, Infrastructure]
```
Dependencies point inward: Api → Application/Infrastructure → Domain. Domain depends on nothing. That's what Demo 1's architecture diagram should show.

## Run it
```bash
cd ShopApi
dotnet run --project ShopApi.Api
# Swagger at the printed URL (…/swagger)
```
The SQLite DB (`shop.db`) is created and seeded on startup (1 customer + 3 products).

## Try the endpoints
```bash
# list products
curl http://localhost:5080/api/products

# create an order  (Controller -> CreateOrderHandler -> Repositories -> AppDbContext -> DB)
curl -X POST http://localhost:5080/api/orders \
  -H "Content-Type: application/json" \
  -d '{"customerId":1,"items":[{"productId":1,"quantity":2},{"productId":3,"quantity":1}]}'
# -> {"orderId":1,"total":279.90}
```

## The three prompts to run on the board (during recording)
1. **Architecture:** *"Analyze this .NET solution's structure and create an architecture diagram on my Miro board [BOARD_URL]. Show each project as a layer and draw the dependencies between them."*
2. **ER model:** *"Read my EF Core DbContext and entity classes, and create an ER diagram on the Miro board showing the tables, columns, keys, and relationships."*
3. **Sequence:** *"Trace the 'Create Order' request flow from the controller through the handler to the database, and draw it as a sequence diagram on the Miro board."*

## Entities / relationships (what the ER diagram should show)
- **Customer** 1—* **Order**
- **Order** 1—* **OrderItem**
- **Product** 1—* **OrderItem**
- `Order.Status` is an `OrderStatus` enum; `Order.Total` is computed (not stored).

> Requires the .NET 8 SDK. If you use a different version, adjust the `TargetFramework` and EF Core package versions.
