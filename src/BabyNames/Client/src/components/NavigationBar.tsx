import { solid } from '@fortawesome/fontawesome-svg-core/import.macro';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React from 'react';
import { Link } from 'react-router-dom';
import styles from './NavigationBar.css';

function NavigationBar() {
	return (
		<nav className={styles.root}>
			<Link to="/" className={styles.title}>
				<h2>Baby Names</h2>
			</Link>
			<a href="/login/logout" className={styles.logout}>
				<FontAwesomeIcon icon={solid('arrow-right-from-bracket')} />
			</a>
		</nav>
	);
}

export default NavigationBar;
