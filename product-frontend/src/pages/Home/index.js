import {Button, Card, Container, Row, Col} from 'react-bootstrap';
import React, {   useContext } from 'react';
import { SearchContext } from '../../components/Layout/DefaultLayout/Header/SearchContext';
import styles from './Home.module.css'

function Home() {
    // const [products, setProducts] = useState([]);
    
  
    // useEffect(() => {
    //   getProducts().then(response => {
    //     setProducts(response.data);
    //   });
    // }, []);

    const { products } = useContext(SearchContext);
  
    return (
      <div>
          <Container>
            <Row>
            {products.map(product => (
                <Col>
                    <Card className={styles['prd-card']}>
                    <Card.Img variant="top" src={product.image} />
                    <Card.Body>
                    <Card.Title className={styles['prd-name']}>
                      {product.name}
                    </Card.Title>
                    <div className={styles['price-and-quan']}>
                      <Card.Title className={styles['prd-price']}>
                        {product.price + 'đ'}
                      </Card.Title>
                      <Card.Title className={styles['prd-quan']}>
                        {'Số lượng: ' + product.quantity}
                      </Card.Title>
                    </div>
                    <Card.Text className={styles['prd-desc']}>
                        {product.description}
                    </Card.Text>
                    <Button variant="primary">Đặt hàng</Button>
                    </Card.Body>
                    </Card>
                </Col>
            ))}
            </Row>
          </Container>
      </div>
    );
  };

export default Home;