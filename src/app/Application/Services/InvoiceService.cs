using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Requests;
using Domain.Products;
using Domain.Branches;
using Domain.Invoices;

namespace Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IProductRepository _productRepository;

    public InvoiceService(IInvoiceRepository invoiceRepository, IBranchRepository branchRepository, IProductRepository productRepository)
    {
        _invoiceRepository = invoiceRepository;
        _branchRepository = branchRepository;
        _productRepository = productRepository;
    }

    public async Task<InvoiceDTO?> GetByIdAsync(Guid id)
    {
        Invoice? invoice = await _invoiceRepository.GetByIdAsync(id);

        if (invoice is null)
        {
            return null;
        }

        Branch branch = invoice.BranchInformations;

        return new InvoiceDTO
        {
            RowGuid = invoice.RowGuid,
            BranchInformations = new BranchDTO
                {
                    Id = branch.Id,
                    BranchName = branch.BranchName,

                    Products = branch.Products
                        .Select(product => new BranchProductDTO
                        {
                            Id = product.Id,
                            ProductName = product.ProductName,
                            ProductPriceWithTax = product.ProductPriceWithTax ?? 0m,
                            ProductCount = product.ProductCount
                        })
                        .ToList(),

                    InvoiceId = branch.InvoiceId,
                    UpdatedAt = branch.UpdatedAt,
                    CreatedAt = branch.CreatedAt,
                },

            InvoiceTerm = invoice.InvoiceTerm,
            InvoiceYear = invoice.InvoiceYear,
            CreatedAt = invoice.CreatedAt,
        };
    }

    public async Task<IReadOnlyList<InvoiceDTO>> GetAllAsync()
    {
        List<Invoice?> invoices = await _invoiceRepository.GetAllAsync();

        return invoices.Select(invoice =>
        {
            Branch branch = invoice.BranchInformations;

            return new InvoiceDTO{

                RowGuid = invoice.RowGuid,

                BranchInformations = new BranchDTO
                    {
                        Id = branch.Id,
                        BranchName = branch.BranchName,

                        Products = branch.Products
                            .Select(product => new BranchProductDTO
                            {
                                Id = product.Id,
                                ProductName = product.ProductName,
                                ProductPriceWithTax = product.ProductPriceWithTax ?? 0m,
                                ProductCount = product.ProductCount
                            })
                            .ToList(),

                        InvoiceId = branch.InvoiceId,
                        UpdatedAt = branch.UpdatedAt,
                        CreatedAt = branch.CreatedAt,
                    },

                InvoiceTerm = invoice.InvoiceTerm,
                InvoiceYear = invoice.InvoiceYear,
                CreatedAt = invoice.CreatedAt,
            };
        })
        .ToList();
    }

    public async Task<InvoiceDTO> CreateAsync(CreateInvoiceRequest request)
    {
        Branch branch = await _branchRepository.GetByIdAsync(request.BranchId);

        Invoice invoice = new Invoice
        {
            RowGuid = Guid.NewGuid(),
            InvoiceName = request.InvoiceName,
            BranchId = request.BranchId,  
            BranchInformations = branch,
            InvoiceTerm = request.InvoiceTerm,
            InvoiceYear = request.InvoiceYear
        };
        
        await _invoiceRepository.AddAsync(invoice);

        return new InvoiceDTO
        {
            RowGuid = invoice.RowGuid,
            BranchInformations = new BranchDTO
                {
                    Id = branch.Id,
                    BranchName = branch.BranchName,

                    Products = branch.Products
                        .Select(product => new BranchProductDTO
                        {
                            Id = product.Id,
                            ProductName = product.ProductName,
                            ProductPriceWithTax = product.ProductPriceWithTax ?? 0m,
                            ProductCount = product.ProductCount
                        })
                        .ToList(),

                    InvoiceId = branch.InvoiceId,
                    UpdatedAt = branch.UpdatedAt,
                    CreatedAt = branch.CreatedAt,
                },

            InvoiceTerm = invoice.InvoiceTerm,
            InvoiceYear = invoice.InvoiceYear,
            CreatedAt = invoice.CreatedAt,
        };

    }

    public async Task<InvoiceDTO?> UpdateAsync(UpdateInvoiceRequest request)
    {
        Invoice? invoice = await _invoiceRepository.GetByIdAsync(request.InvoiceId);

        if (invoice is null)
        {
            return null;
        }

        invoice.InvoiceName = request.InvoiceName;

        _invoiceRepository.Update(invoice);
        
        Branch branch = invoice.BranchInformations;

        return new InvoiceDTO
        {
            RowGuid = invoice.RowGuid,
            BranchInformations = new BranchDTO
                {
                    Id = branch.Id,
                    BranchName = branch.BranchName,

                    Products = branch.Products
                        .Select(product => new BranchProductDTO
                        {
                            Id = product.Id,
                            ProductName = product.ProductName,
                            ProductPriceWithTax = product.ProductPriceWithTax ?? 0m,
                            ProductCount = product.ProductCount
                        })
                        .ToList(),

                    InvoiceId = branch.InvoiceId,
                    UpdatedAt = branch.UpdatedAt,
                    CreatedAt = branch.CreatedAt,
                },

            InvoiceTerm = invoice.InvoiceTerm,
            InvoiceYear = invoice.InvoiceYear,
            CreatedAt = invoice.CreatedAt,
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Invoice? invoice = await _invoiceRepository.GetByIdAsync(id);

        if(invoice is null)
        {
            return false;
        }

        _invoiceRepository.Remove(invoice);

        return true;
    }

}