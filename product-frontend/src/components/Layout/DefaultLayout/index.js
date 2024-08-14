import React from 'react';
import Header from './Header'
import Sidebar from './Sidebar'
import styles from './DefaultLayout.module.css'

function DefaultLayout({children}) {
    return ( 
        <div className={styles['wrapper']}>
            <Header/>
            <div className={styles['container']}>
                <Sidebar/>
                <div className='content'>
                    {children}
                </div>
            </div>
        </div>
     );
}

export default DefaultLayout;