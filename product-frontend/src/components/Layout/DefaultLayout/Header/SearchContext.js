import React, { createContext, useState, useEffect } from 'react';
import { getProducts } from '../../../../services/productService';

export const SearchContext = createContext();

export const SearchProvider = ({ children }) => {
  const [searchTerm, setSearchTerm] = useState('');
  const [price, setPrice] = useState({'minPrice': 0, 'maxPrice': 1000000000});
  const [quantity, setQuantity] = useState(0);
  const [products, setProducts] = useState([]);

  useEffect(() => {
    getProducts().then(response => {
      setProducts(response.data);
    })
  }, []);
  return (
    <SearchContext.Provider value={{ searchTerm, setSearchTerm, price, setPrice, quantity, setQuantity, products, setProducts }}>
      {children}
    </SearchContext.Provider>
  );
};