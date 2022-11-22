import React, { useEffect, useState } from 'react';
import { bindActionCreators } from 'redux';
import LoadingSpinner from '~/components/LoadingSpinner';
import { NameGender } from '~/models';
import { useAppDispatch, useAppSelector } from '~/redux';
import CompareResultsForm from './CompareResultsForm';
import resultsSlice from './results.slice';
import ResultsList from './ResultsList';
import styles from './ResultsPage.css';

function ResultsPage() {
	const dispatch = useAppDispatch();
	const { getCompletedBabyNames, compareResults } = bindActionCreators(
		resultsSlice.actions,
		dispatch
	);

	const [gender] = useState<NameGender>(null);

	useEffect(() => {
		getCompletedBabyNames({ gender });
	}, [gender]);

	const isLoading = useAppSelector((state) => state.results.isLoading);
	const completedNames = useAppSelector(
		(state) => state.results.completedNames
	);
	const compareResult = useAppSelector((state) => state.results.compareResult);

	if (isLoading) {
		return <LoadingSpinner />;
	}

	const compareToUser = (emailAddress: string) => {
		compareResults({ targetUserEmail: emailAddress });
	};

	const results = compareResult ? compareResult.matches : completedNames;
	const headerText = compareResult
		? `${compareResult.comparedTo.fullName}'s Matches`
		: 'My Results';

	return (
		<>
			<CompareResultsForm onSubmit={compareToUser} />
			<div className={styles.list}>
				<ResultsList headerText={headerText} names={results} />
			</div>
		</>
	);
}

export default ResultsPage;
