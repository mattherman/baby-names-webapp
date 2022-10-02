import React, { useEffect } from 'react';
import { bindActionCreators } from 'redux';
import namesSlice from './names.slice';
import { NameGender, Vote } from '~/models';
import { useAppDispatch, useAppSelector } from '~/redux';

function NamesPage() {
	const dispatch = useAppDispatch();
	const { getRemainingBabyNames, submitVote } = bindActionCreators(
		namesSlice.actions,
		dispatch
	);
	useEffect(() => {
		getRemainingBabyNames(NameGender.Male);
	}, []);
	const { currentName, isLoading } = useAppSelector((state) => state.names);

	if (isLoading) {
		return (
			<div>Loading...</div>
		);
	};

	if (currentName === null) {
		return (
			<div>All done!</div>
		);
	}

	const createVoteClickHandler = (vote: Vote) => () => {
		submitVote({ id: currentName.id, vote: vote });
	}

	return (
		<>
			<h1>{currentName.name}</h1>
			<button onClick={createVoteClickHandler(Vote.Nay)}>No</button>
			<button onClick={createVoteClickHandler(Vote.Yea)}>Yes</button>
		</>
	);
}

export default NamesPage;
