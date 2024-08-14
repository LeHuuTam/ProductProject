import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import DefaultLayout from './components/Layout/DefaultLayout';
import AdminLayout from './components/Layout/AdminLayout';
import Home from './pages/Home'
import Login from './pages/Login'
import Admin from './pages/Admin'
import CreateProduct from './pages/CreateProduct';
import UpdateProduct from './pages/UpdateProduct';


import { SearchProvider } from './components/Layout/DefaultLayout/Header/SearchContext';


const App = () => {
  return (
    <SearchProvider>
      <Router>
        <div>
          <Routes>
            <Route path="/" element={
              <DefaultLayout>
                <Home/>
              </DefaultLayout>} />

            <Route path="/login" element={<Login/>} />

            <Route path="/admin" element={
              <AdminLayout>
                <Admin/>
              </AdminLayout>} />
            
            <Route path="/admin/product/create" element={
              <AdminLayout>
                <CreateProduct/>
              </AdminLayout>} />

            <Route path="/admin/product/update/:id" element={
              <AdminLayout>
                <UpdateProduct/>
              </AdminLayout>} />
          </Routes>
        </div>
      </Router>
    </SearchProvider>
  );
};

export default App;
