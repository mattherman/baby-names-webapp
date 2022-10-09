import React from 'react';
import styles from './Layout.css';

interface ILayoutProps {
	children: JSX.Element;
}

function Layout({ children }: ILayoutProps) {
	return <div className={styles.container}>{children}</div>;
}

export default Layout;
