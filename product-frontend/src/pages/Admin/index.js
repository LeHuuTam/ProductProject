import React, {useContext} from 'react';
import { useNavigate } from 'react-router-dom';
import {Button, Table} from 'react-bootstrap'
import { SearchContext } from '../../components/Layout/DefaultLayout/Header/SearchContext';
import styles from './Admin.module.css'
import { getProducts, deleteProduct } from '../../services/productService';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faPencil, faTrashCan } from '@fortawesome/free-solid-svg-icons';

function Admin() {
    // const { products } = useContext(SearchContext);
    const navigate = useNavigate();

    if(!localStorage.getItem('token')){
        navigate('/login');
    }
    const { searchTerm, setSearchTerm, products, setProducts } = useContext(SearchContext);
    const deletePrd = (id) =>{
        const confirmDelete = window.confirm(`Xác nhận xoá sản phẩm ID = ${id}`);
        if(confirmDelete){
            deleteProduct(id).then(respone => {
                if(respone.status === 200){
                    getProducts().then(response => {
                        setProducts(response.data);
                      });
                }
            }).catch(() => {
                alert('Không thể xoá sản phẩm!');
            });
        }
    }

    const handleUpdateClick = (id) => {
        navigate(`/admin/product/update/${id}`);
    };

    return (
        <div className={styles['admin-table-container']}>
            <Button variant="success" className={styles['admin-add-btn']} onClick={() => {navigate('/admin/product/create')}}>Thêm sản phẩm mới</Button>{' '}
            <Table striped="columns" className={styles['admin-table']}>
                <thead>
                    <tr>
                    <th>#</th>
                    <th>Ảnh</th>
                    <th>Tên sản phẩm</th>
                    <th>Giá</th>
                    <th>Số lượng</th>
                    <th>Mô tả</th>
                    <th>Cập nhật</th>
                    <th>Xoá</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map(product => (
                        <tr className={styles['admin-row']}>
                            <td>{product.id}</td>
                            <td>
                                <img className={styles['admin-img']} src={product.image}/>
                            </td>
                            <td>
                                <h3>{product.name}</h3>
                            </td>
                            <td>
                                {product.price}
                            </td>
                            <td className={styles['quan-col']}>
                                {product.quantity}</td>
                            <td>
                                <div className={styles['admin-desc']}>
                                    {product.description}
                                </div>    
                            </td>
                            <td className={styles['upd-col']}>
                                <FontAwesomeIcon icon={faPencil} onClick={() => handleUpdateClick(product.id)}/>
                            </td>
                            <td className={styles['del-col']}>
                                <FontAwesomeIcon icon={faTrashCan} style={{color: "#b10b0b",}} onClick={() => deletePrd(product.id)}/>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    );
}

export default Admin;