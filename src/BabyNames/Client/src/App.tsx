import React, { useEffect } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import LoadingSpinner from './components/LoadingSpinner';
import Layout from './Layout';
import MenuPage from './menu/MenuPage';
import { NameGender } from './models';
import { useAppDispatch, useAppSelector } from './redux';
import ResultsPage from './results/ResultsPage';
import sessionSlice from './session/session.slice';
import VotingPage from './voting/VotingPage';

function App() {
	const dispatch = useAppDispatch();
	const { getToken } = bindActionCreators(sessionSlice.actions, dispatch);
	useEffect(() => {
		getToken();
	}, []);

	const token = useAppSelector((state) => state.session.token);
	if (!token) {
		return (
			<Layout>
				<LoadingSpinner />
			</Layout>
		);
	}

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
