import React, { useEffect, useState } from 'react';
import { bindActionCreators } from 'redux';
import LoadingSpinner from '~/components/LoadingSpinner';
import { NameGender, Vote } from '~/models';
import { useAppDispatch, useAppSelector } from '~/redux';
import ResultRow from './ResultRow';
import resultsSlice from './results.slice';
import styles from './ResultsPage.css';

function ResultsPage() {
	const dispatch = useAppDispatch();
	const { getCompletedBabyNames } = bindActionCreators(
		resultsSlice.actions,
		dispatch
	);

	const [gender] = useState<NameGender>(null);
	const [showRejectedNames] = useState<boolean>(false);

	useEffect(() => {
		getCompletedBabyNames({ gender });
	}, [gender]);

	const completedNames = useAppSelector(
		(state) => state.results.completedNames
	);
	const isLoading = useAppSelector((state) => state.results.isLoading);

	if (isLoading) {
		return <LoadingSpinner />;
	}

	const filteredNames = showRejectedNames
		? completedNames
		: completedNames.filter((name) => name.vote == Vote.Yea);

	if (filteredNames.length === 0) {
		return <div className={styles.empty}>No results</div>;
	}

	return (
		<div className={styles.root}>
			{filteredNames.map((name) => (
				<ResultRow key={name.id} babyName={name} />
			))}
		</div>
	);
}

export default ResultsPage;
