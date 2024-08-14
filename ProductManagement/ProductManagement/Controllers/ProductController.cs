using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductManagement.Commands;
using ProductManagement.Queries;
using ProductManagement.Validators;
using System;

namespace ProductManagement.Controllers
{
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateProductCommand> _createProductValidator;
        private readonly IValidator<UpdateProductCommand> _updateProductValidator;
        private readonly IValidator<DeleteProductCommand> _deleteProductValidator;
        private readonly IValidator<FilterByPriceProductQuery> _filterByPriceProductValidator;
        private readonly IValidator<FilterByQuantityProductQuery> _filterByQuantityProductValidator;
        private readonly IValidator<GetByIdProductQuery> _getByIdProductValidator;

        public ProductController(IMediator mediator,
            IValidator<CreateProductCommand> createProductValidator,
            IValidator<UpdateProductCommand> updateProductValidator,
            IValidator<DeleteProductCommand> deleteProductValidator,
            IValidator<FilterByPriceProductQuery> filterByPriceProductValidator,
            IValidator<FilterByQuantityProductQuery> filterByQuantityProductValidator,
            IValidator<GetByIdProductQuery> getByIdProductValidator)
        {
            _mediator = mediator;
            _createProductValidator = createProductValidator;
            _updateProductValidator = updateProductValidator;
            _deleteProductValidator = deleteProductValidator;
            _filterByPriceProductValidator = filterByPriceProductValidator;
            _filterByQuantityProductValidator = filterByQuantityProductValidator;
            _getByIdProductValidator = getByIdProductValidator;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll(GetAllProductQuery getAllProductQuery)
        {
            try
            {
                var productList = await _mediator.Send(getAllProductQuery);
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("getbyid/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQuery getByIdProductQuery)
        {
            var validate = await _getByIdProductValidator.ValidateAsync(getByIdProductQuery);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors);
            }
            try
            {
                var product = await _mediator.Send(getByIdProductQuery);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchProductQuery searchProductQuery)
        {
            try
            {
                var productList = await _mediator.Send(searchProductQuery);
                return Ok(productList);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("filterbyprice")]
        public async Task<IActionResult> FilterByPrice([FromQuery] FilterByPriceProductQuery filterByPriceProductQuery)
        {
            var validate = await _filterByPriceProductValidator.ValidateAsync(filterByPriceProductQuery);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors);
            }
            try
            {
                var productList = await _mediator.Send(filterByPriceProductQuery);
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("filterbyquantity")]
        public async Task<IActionResult> FilterByQuantity([FromQuery] FilterByQuantityProductQuery filterByQuantityProductQuery)
        {
            var validate = await _filterByQuantityProductValidator.ValidateAsync(filterByQuantityProductQuery);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors);
            }
            try
            {
                var productList = await _mediator.Send(filterByQuantityProductQuery);
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] CreateProductCommand createProductCommand)
        {
            var validate = await _createProductValidator.ValidateAsync(createProductCommand);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors);
            }
            try
            {
                var product = await _mediator.Send(createProductCommand);
                return Ok(product);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] UpdateProductCommand updateProductCommand)
        {
            var validate = await _updateProductValidator.ValidateAsync(updateProductCommand);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors);
            }
            try
            {
                var product = await _mediator.Send(updateProductCommand);
                return Ok(product);
            }
            catch( Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpPost("delete")]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] DeleteProductCommand deleteProductCommand)
        {
            var validate = await _deleteProductValidator.ValidateAsync(deleteProductCommand);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors);
            }
            try
            {
                var product = await _mediator.Send(deleteProductCommand);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
