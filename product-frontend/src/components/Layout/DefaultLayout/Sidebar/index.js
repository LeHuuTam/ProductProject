import React, {useContext, useState} from 'react';
import styles from './Sidebar.module.css'
import {Button, Form} from 'react-bootstrap'
import { SearchContext } from '../Header/SearchContext';
import { getProductByPrice, getProductByQuantity } from '../../../../services/productService';

function Sidebar() {
    const { price, setPrice, quantity, setQuantity, setProducts } = useContext(SearchContext);
    const [checkedItems, setCheckedItems] = useState({});
    const handleClickCheckbox = (event) => {
        setCheckedItems({
            ...checkedItems,
            [event.target.name]: event.target.checked
          });
    }



        // if(minPrice < price.minPrice) price.minPrice = minPrice;
        // if(maxPrice > price.maxPrice) price.maxPrice = maxPrice;
        // setPrice(() => price);
    
    const handleApplyPriceFilter = () =>{
        const checked = Object.keys(checkedItems).filter(key => checkedItems[key]);
        console.log('Checked items:', checked);
        var a = {
            minPriceArr : [],
            maxPriceArr : []
        }
        if(checked.includes('item1')){
            a.minPriceArr.push(0);
            a.maxPriceArr.push(5000000);
        }
        if(checked.includes('item2')){
            a.minPriceArr.push(5000000);
            a.maxPriceArr.push(10000000);
        }
        if(checked.includes('item3')){
            a.minPriceArr.push(1000000);
            a.maxPriceArr.push(20000000);
        }
        if(checked.includes('item4')){
            a.minPriceArr.push(20000000);
            a.maxPriceArr.push(1000000000);
        }
        var minPrice = Math.min.apply(Math, a.minPriceArr);
        var maxPrice = Math.max.apply(Math, a.maxPriceArr);
        console.log('min: ' + minPrice);
        console.log('max: ' + maxPrice);
        var newPrice = {'minPrice' : minPrice, 'maxPrice': maxPrice};
        getProductByPrice(newPrice).then(response => {
            setProducts(response.data);
        });
    }
    const handleApplyQuantityFilter = () =>{
        getProductByQuantity(quantity).then(response => {
            setProducts(response.data);
        });
    }
    return ( 
        <aside className={styles['wrapper']}>
            
            <Form style={{marginLeft:'10px', marginTop:'20px'}}>
                <h3>Lọc theo giá</h3>
                {['checkbox'].map((type) => (
                    <div key={`default-${type}`} className={styles['price-filter']}>
                        <Form.Check className={styles['price-check']}
                            onChange={handleClickCheckbox}
                            checked={checkedItems['item1'] || false}
                            name="item1"
                            type={type}
                            id={`default-${type}`}
                            label='Dưới 5 triệu'
                        />
                        <Form.Check className={styles['price-check']}
                            onChange={handleClickCheckbox}
                            checked={checkedItems['item2'] || false}
                            name="item2"
                            type={type}
                            id={`default-${type}`}
                            label='5 triệu - 10 triệu'
                        />
                        <Form.Check  className={styles['price-check']}
                            onChange={handleClickCheckbox}
                            checked={checkedItems['item3'] || false}
                            name="item3"
                            type={type}
                            id={`default-${type}`}
                            label='10 triệu - 20 triệu'
                        />
                        <Form.Check className={styles['price-check']}
                            onChange={handleClickCheckbox}
                            checked={checkedItems['item4'] || false}
                            name="item4"
                            type={type}
                            id={`default-${type}`}
                            label='Trên 20 triệu'
                        />
                    </div>
                ))}
                <Button variant="info" className={styles['apply-btn']} onClick={handleApplyPriceFilter}>Áp dụng</Button>{' '}
            </Form>
            <Form style={{marginLeft:'10px', marginTop:'20px'}}>
                <h3>Lọc theo số lượng</h3>
                <div className={styles['price-filter']}>
                        <Form.Control type="number" className={styles['quantity-input']} value={quantity} onChange={(e) => {setQuantity(e.target.value); console.log(quantity);}}/>
                </div>
                <Button variant="info" className={styles['apply-btn']} onClick={handleApplyQuantityFilter}>Áp dụng</Button>{' '}
            </Form>
        </aside>
     );
}

export default Sidebar;