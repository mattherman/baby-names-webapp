import React from 'react';
import { useNavigate } from 'react-router-dom';
import Button from '~/components/Button';
import styles from './MenuPage.css';

function MenuPage() {
	const navigate = useNavigate();
	return (
		<div className={styles.root}>
			<Button onClick={() => navigate('boy-names')}>Choose Boy Names</Button>
			<Button onClick={() => navigate('girl-names')}>Choose Girl Names</Button>
			<Button onClick={() => navigate('results')}>Results</Button>
		</div>
	);
}

export default MenuPage;
