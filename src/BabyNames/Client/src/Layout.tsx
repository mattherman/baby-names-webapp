import React from 'react';
import cx from 'classnames';
import styles from './Layout.css';

interface ILayoutProps {
	children: JSX.Element | JSX.Element[];
	centered?: boolean;
}

function Layout({ children, centered }: ILayoutProps) {
	const classes = cx({
		[styles.container]: true,
		[styles.centered]: centered,
		[styles.nonCentered]: !centered,
	});
	return <div className={classes}>{children}</div>;
}

export default Layout;
