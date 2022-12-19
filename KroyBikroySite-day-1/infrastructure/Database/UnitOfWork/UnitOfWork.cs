
using infrastructure.Database.StoreContext;

using core.Entities.Model;
using infrastructure.Database.Repository;
using core.Interfaces;
using infrastructure.Database.Generic;

namespace infrastructure.Database.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;


        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IProjectRepository ProjectRepository => new ProjectRepository(_context);

        public IRepository<Location> LocationRepository => new Repository<Location>(_context);

        public IRepository<Category> CategoryRepository => new  Repository<Category>(_context);

        public IRepository<AffiliatedAgency> AffiliatedAgencyRepository => new Repository<AffiliatedAgency>(_context);





        // public IRepository<User> UserRepository => new Repository<User>(_context);
        // public ICustomerRepository Customer => throw new NotImplementedException();
        // public IRepository<ProductType> TypeRepository => new Repository<ProductType>(_context);

        // public IProductRepository ProductRepository { get; private set; }
        // public IRepository<Picture> ProductPictureRepository => new Repository<Picture>(_context);

        // public IRepository<ProductBid> ProductBidRepository => new Repository<ProductBid>(_context);

        // public IRepository<ProductRatting> ProductRating => new Repository<ProductRatting>(_context);

        // public IRepository<PaymentRequest> PaymentRequest => new Repository<PaymentRequest>(_context);

        // public IRepository<ProductSold> ProductSold => new Repository<ProductSold>(_context);



        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task RollbackAsync()
        {
            await _context.DisposeAsync();
        }
    }
}