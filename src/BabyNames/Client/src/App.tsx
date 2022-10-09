import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Layout from './Layout';
import MenuPage from './menu/MenuPage';
import { NameGender } from './models';
import ResultsPage from './results/ResultsPage';
import VotingPage from './voting/VotingPage';

function App() {
	return (
		<Layout>
			<BrowserRouter>
				<Routes>
					<Route index element={<MenuPage />} />
					<Route
						path="boy-names"
						element={<VotingPage gender={NameGender.Male} />}
					/>
					<Route
						path="girl-names"
						element={<VotingPage gender={NameGender.Female} />}
					/>
					<Route path="results" element={<ResultsPage />} />
				</Routes>
			</BrowserRouter>
		</Layout>
	);
}

export default App;
