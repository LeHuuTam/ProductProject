import React, {useContext} from 'react';
import { useNavigate } from 'react-router-dom';
import styles from './Header.module.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import {Button} from 'react-bootstrap';
import { SearchContext } from './SearchContext';
import { searchProducts } from '../../../../services/productService';

function Header() {
    const { searchTerm, setSearchTerm, setProducts } = useContext(SearchContext);
    const navigate = useNavigate();
    const handleSearchClick = () => {
        if (searchTerm) {
            searchProducts(searchTerm).then(response => {
                setProducts(response.data);
              });
        }
    }
    const handleManageClick = () => {
        if(localStorage.getItem('token')){
            navigate('/admin');
        }
        else{
            navigate('/login');
        }
    }
    const handleLoginClick = () => {
        navigate('/login');
    }
    const handleLogoutClick = () => {
        localStorage.removeItem('token');
        navigate('/');
    }
    return ( 
        <header className={styles['wrapper']}>
            <div className={styles['inner']}>
                <div className={styles['logo']}>
                    <img src='/logo.png'></img>
                </div>
                <div className={styles['searchbox']}>
                    <input className={styles['search']} value={searchTerm} onChange={(e) => setSearchTerm(e.target.value)}>
                    </input>
                    <Button variant="info" style={{borderRadius:'12px', width:'16%'}} onClick={handleSearchClick}>Search</Button>{' '}
                </div>
                <div className={styles['login-link']}>
                    <a href='/'>Trang chủ</a>
                </div>
                <div className={styles['login-link']}>
                    <a href='' onClick={handleManageClick}>Quản lý sản phẩm</a>
                </div>
                <div className={styles['login-link']}>
                    {localStorage.getItem('token') == null? <a href='' onClick={handleLoginClick}>Đăng nhập</a> : <a href='' onClick={handleLogoutClick}>Đăng xuất</a>}
                    
                </div>
            </div>
        </header>
     );
}

export default Header;