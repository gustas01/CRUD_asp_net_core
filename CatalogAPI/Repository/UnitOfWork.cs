using CatalogAPI.Context;

namespace CatalogAPI.Repository;

public class UnitOfWork : IUnitOfWork {
  private IProductRepository? _productRepository;

  private ICategoryRepository? _categoryRepository;
  public CatalogAPIContext _context;

  public UnitOfWork(CatalogAPIContext context){
    _context = context;
  }


  public IProductRepository ProductRepository {
    get { return _productRepository = _productRepository ?? new ProductRepository(_context);}
  }

  ICategoryRepository IUnitOfWork.CategoryRepository {
    get { return _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);}
  }

  public async Task Commit(){
    await _context.SaveChangesAsync();
  }

  public void Dispose(){
    _context.Dispose();
  }
}