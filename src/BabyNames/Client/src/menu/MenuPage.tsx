import React from 'react';
import { Link } from 'react-router-dom';

function MenuPage() {
	return (
		<ul>
			<li>
				<Link to="boy-names">Choose Boy Names</Link>
			</li>
			<li>
				<Link to="girl-names">Choose Girl Names</Link>
			</li>
			<li>
				<Link to="results">My Names</Link>
			</li>
		</ul>
	);
}

export default MenuPage;
