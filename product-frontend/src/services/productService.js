import axios from 'axios';

const PRODUCT_URL = 'https://productmanagement20240814103858.azurewebsites.net/product';
const token = localStorage.getItem('token');

export const getProducts = () => axios.get(`${PRODUCT_URL}/getall`);
export const searchProducts = (text) => axios.get(`${PRODUCT_URL}/search?text=${text}`);
export const getProductById = (id) => axios.get(`${PRODUCT_URL}/getbyid/${id}`);
export const getProductByPrice = (price) => axios.get(`${PRODUCT_URL}/filterbyprice?minprice=${price.minPrice}&maxprice=${price.maxPrice}`);
export const getProductByQuantity = (quantity) => axios.get(`${PRODUCT_URL}/filterbyquantity?quantity=${quantity}`);

export const createProduct = (formData) => axios.post(
    `${PRODUCT_URL}/create`,
    formData,
    {
        headers: {
            'Authorization': `Bearer ${token}`
        }
    });
export const updateProduct = (formData) =>  axios.post(
    `${PRODUCT_URL}/update`,
    formData,
    {
        headers: {
            'Authorization': `Bearer ${token}`
        }
    });

export const deleteProduct = (id) => axios.post(
    `${PRODUCT_URL}/delete`,
    {id},
    {
        headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json',
        },
    });