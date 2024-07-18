

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersManagement.Core.Dtos;
using OrdersManagement.APIs.Errors;
using OrdersManagement.APIs;
using OrdersManagement.Core.Entities;
using OrdersManagement.Repository.Data;
using OrdersManagement.Core.Repositories.Interfaces;
using OrdersManagement.Core;

namespace OrdersManagement.APIs.Controllers
{
	[Authorize]
    /// <summary>
    /// API Controller to manage Products 
    /// </summary>
    public class ProductsController : BaseApiController

	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly OrdersDbContext _context;
		private readonly IProductRepository _productRepository;

		public ProductsController(IMapper mapper, IUnitOfWork unitOfWork, OrdersDbContext context, IProductRepository productRepository)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_context = context;
			_productRepository = productRepository;
		}


        #region GetAllProducts
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        [HttpGet]
		[ProducesResponseType(typeof(List<ProductDto>), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
		{


			var Products = await _unitOfWork.Repository<Product>().GetAllAsync();


			return Ok(Products);
		} 
		#endregion

		#region GetProductById
		/// <summary>
		/// Retrieves a specific product by ID.
		/// </summary>
		/// <param name="id">The ID of the product to retrieve.</param>
		/// <returns>The product details.</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductDto), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<ProductDto>> GetProductById(int id)
		{
			var Product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);

			if (Product is null)
				return NotFound(new ApiResponse(400));

			return Ok(Product);

		} 
		#endregion

		#region AddProduct
		/// <summary>
		/// Adds a new product.
		/// </summary>
		/// <param name="model">The product details to add.</param>
		/// <returns>The added product details.</returns>
		[HttpPost]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ProductDto), 200)]
		[ProducesResponseType(typeof(ApiResponse), 400)]
		public async Task<ActionResult<ProductDto>> AddProduct([FromBody] ProductDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var product = new Product()
			{
				Name = model.Name,
				Price = model.Price,
				Stock = model.Stock
			};

			await _unitOfWork.Repository<Product>().Add(product);
			var ReturnedProduct = new ProductDto
			{
				Name = model.Name,
				Price = model.Price,
				Stock = model.Stock
			};

			return Ok(ReturnedProduct);
		} 
		#endregion

		#region UpdateProduct
		/// <summary>
		/// Updates an existing product.
		/// </summary>
		/// <param name="updatedProduct">The updated product details.</param>
		/// <returns>The updated product details.</returns>
		[HttpPut]
		[Authorize(Roles = "Admin")]
		[ProducesResponseType(typeof(ProductDto), 200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> UpdateProduct(/*int productId, [FromBody]*/ ProductDto updatedProduct)
		{
			if (updatedProduct == null)
			{
				return BadRequest();
			}

			var existingProduct = _productRepository.GetProductById(updatedProduct.Id);
			if (existingProduct == null)
			{
				return NotFound();
			}

			existingProduct.Name = updatedProduct.Name;
			existingProduct.Price = updatedProduct.Price;
			existingProduct.Stock = updatedProduct.Stock;

			await _productRepository.UpdateProductAsync(existingProduct);
			return Ok(existingProduct);
		}

		#endregion

	}


}

