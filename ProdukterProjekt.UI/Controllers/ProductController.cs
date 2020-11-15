﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdukterProjekt.Core.ApplicationService;
using ProdukterProjekt.Core.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProdukterProjekt.UI.Controllers
{
    /// <summary>
    /// Product Controller responsible for GET/POST for managing products
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productservice;

        public ProductController(IProductService productService)
        {
            _productservice = productService;
        }

        // GET: api/<ProductController>
        [Authorize]
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productservice.GetProducts();
        }

        /// <summary>
        /// This GET method return products
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Array of Products</returns>
        // GET api/<ProductController>/5
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _productservice.GetProductByID(id);
        }

        // POST api/<ProductController>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public void Post([FromBody] Product p)
        {
            _productservice.CreateProduct(p);
        }

        // PUT api/<ProductController>/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product p)
        {
            _productservice.UpdateProduct(id, p);
        }

        // DELETE api/<ProductController>/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productservice.DeleteProduct(id);
        }
    }
}
