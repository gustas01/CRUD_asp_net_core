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

  public void Commit(){
    _context.SaveChanges();
  }

  public void Dispose(){
    _context.Dispose();
  }
}