import React, {useEffect, useState, useContext} from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import {Button, Form} from 'react-bootstrap'
import styles from './UpdateProduct.module.css'
import { getProducts, getProductById, updateProduct } from '../../services/productService';
import { SearchContext } from '../../components/Layout/DefaultLayout/Header/SearchContext';


function UpdateProduct() {
    const navigate = useNavigate();
    if(!localStorage.getItem('token')){
        navigate('/login');
    }
    const { id } = useParams();
    const [product, setProduct] = useState('');
    const [selectedFile, setSelectedFile] = useState('');
    const { searchTerm, setSearchTerm, setProducts } = useContext(SearchContext);

    useEffect(() => {
            getProductById(id).then(response => {
                setProduct(response.data);
            })
    }, [id]);
    
    const handleFileChange = (e) => {
        const file = e.target.files[0];
        setSelectedFile(file);
        setProduct((prevProduct) => ({
            ...prevProduct,
            image: URL.createObjectURL(file),
          }));
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        const formData = new FormData();
        Object.entries(product).forEach(([key, value]) => {
            formData.append(key, value);
        });
        if (selectedFile) {
            formData.append('image', selectedFile);
        }
        updateProduct(formData).then(response => {
            console.log(response);
            if(response.status === 200){
                getProducts().then(response => {
                    setProducts(response.data);
                  });
                navigate('/admin');
                alert('Cập nhật sản phẩm thành công');
            }
        }).catch(() => { alert('Không thể cập nhật sản phẩm!');});
    }    


    return (
        <div className={styles['admin-update-container']}>
            <Form className={styles['admin-update-form']} onSubmit={handleSubmit}>
                <h1 className={styles['admin-title']}>Cập nhật sản phẩm</h1>
                <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
                    {/* <Form.Label>Id sản phẩm</Form.Label> */}
                    <Form.Control type="hidden" className={styles['admin-input']} value={product.id}/>
                </Form.Group>
                <Form.Group className="mb-4" controlId="exampleForm.ControlInput2">
                    <Form.Label>Tên sản phẩm</Form.Label>
                    <Form.Control type="text" className={styles['admin-input']} value={product.name} onChange={(e) => setProduct({ ...product, name: e.target.value })}/>
                </Form.Group>
                <Form.Group className="mb-3" controlId="exampleForm.ControlInput3">
                    <Form.Label>Giá</Form.Label>
                    <Form.Control type="text" className={styles['admin-input']} value={product.price} onChange={(e) => setProduct({ ...product, price: e.target.value })}/>
                </Form.Group>
                <Form.Group className="mb-3" controlId="exampleForm.ControlInput4">
                    <Form.Label>Số lượng</Form.Label>
                    <Form.Control type="text" className={styles['admin-input']} value={product.quantity} onChange={(e) => setProduct({ ...product, quantity: e.target.value })}/>
                </Form.Group>
                <Form.Group className="mb-3" controlId="exampleForm.ControlTextarea1">
                    <Form.Label>Mô tả</Form.Label>
                    <Form.Control as="textarea" rows={3} className={styles['admin-input-area']} value={product.description} onChange={(e) => setProduct({ ...product, description: e.target.value })}/>
                </Form.Group>
                <Form.Group controlId="formFile" className="mb-3">
                    <Form.Label>Ảnh</Form.Label>
                    <Form.Control type="file" className={styles['admin-input']} onChange={(handleFileChange)}/>{
                    <div className={styles['admin-temp-img']}>
                        {product.image && <img src={product.image} alt="Current Product" style={{ width: '100px', height: '100px', minHeight:'100px' }} />}
                    </div>}
                </Form.Group>
                <div className={styles['admin-update-btn-box']}>
                    <Button variant="primary" className={styles['admin-update-btn']} type='submit'>Lưu</Button>
                </div>
            </Form>
        </div>
    );
}

export default UpdateProduct;