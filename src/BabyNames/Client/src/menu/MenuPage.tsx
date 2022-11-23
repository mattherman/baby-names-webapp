import React from 'react';
import { useNavigate } from 'react-router-dom';
import Button from '~/components/Button';
import Layout from '~/Layout';
import styles from './MenuPage.css';

function MenuPage() {
	const navigate = useNavigate();
	return (
		<Layout centered>
			<div className={styles.root}>
				<Button onClick={() => navigate('boy-names')}>Choose Boy Names</Button>
				<Button onClick={() => navigate('girl-names')}>
					Choose Girl Names
				</Button>
				<Button onClick={() => navigate('results')}>Results</Button>
			</div>
		</Layout>
	);
}

export default MenuPage;
