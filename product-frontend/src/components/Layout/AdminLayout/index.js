import React from 'react';
import Header from '../DefaultLayout/Header'
import styles from './AdminLayout.module.css'

function DefaultLayout({children}) {
    return ( 
        <div className={styles['wrapper']}>
            <Header/>
            <div className={styles['container']}>
                <div className={styles['content']}>
                    {children}
                </div>
            </div>
        </div>
     );
}

export default DefaultLayout;