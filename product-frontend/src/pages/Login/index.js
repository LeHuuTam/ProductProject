
import {Button, Form, Container, Row, Col} from 'react-bootstrap';
import React, { useEffect, useState, useContext} from 'react';
import {useNavigate} from 'react-router-dom';

import {login} from '../../services/authService'
import styles from './Login.module.css'
function Login() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();
    const handleSubmit = (event) => {
      event.preventDefault();
      login({username, password})
      .then(response => {
        if(response.data.token){
          localStorage.setItem("token", response.data.token);
          navigate('/');
        }
      }).catch(() => {
        var errElm = document.getElementById('error');
        console.log(errElm);
        errElm.classList.remove('Login_hidden-error__2duS0');
      });  
    };
    
    return ( 
        <div className={styles['login-wrapper']}>
            <Container className={styles['login-inner']}>
              <Row className="justify-content-md-center">
                <Col md={4}>
                  <h2 className="text-center">Đăng nhập</h2>
                  <Form onSubmit={handleSubmit}>
                    <Form.Group controlId="formBasicText">
                      <Form.Label>Tên đăng nhập</Form.Label>
                      <Form.Control
                        type="text"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        className={styles['login-username']}
                      />
                    </Form.Group>
                    <Form.Group controlId="formBasicPassword">
                      <Form.Label>Mật khẩu</Form.Label>
                      <Form.Control
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        className={styles['login-pwd']}
                      />
                    </Form.Group>
                    <span id='error' className={styles['hidden-error']}>
                      <h5 className={styles['red-error']}>Tên đăng nhập hoặc mật khẩu không đúng</h5>
                    </span>
                    
                    <Button variant="primary" type="submit" className={styles['login-btn'] + " w-100 mt-3"}>
                      Đăng nhập
                    </Button>
                  </Form>
                </Col>
              </Row>
            </Container>
        </div>
    );
}

export default Login;