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

	const [genderFilter] = useState<NameGender>(null);
	const [showRejectedNames] = useState<boolean>(false);

	useEffect(() => {
		getCompletedBabyNames(genderFilter);
	}, [genderFilter]);

	const { completedNames, isLoading } = useAppSelector(
		(state) => state.results
	);

	if (isLoading) {
		return <LoadingSpinner />;
	}

	const filteredNames = showRejectedNames
		? completedNames
		: completedNames.filter((name) => name.vote == Vote.Yea);

	return (
		<div className={styles.root}>
			{filteredNames.map((name) => (
				<ResultRow key={name.id} babyName={name} />
			))}
		</div>
	);
}

export default ResultsPage;
