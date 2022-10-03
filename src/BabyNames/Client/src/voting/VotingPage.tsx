import React, { useEffect } from 'react';
import { bindActionCreators } from 'redux';
import votingSlice from './voting.slice';
import { NameGender, Vote } from '~/models';
import { useAppDispatch, useAppSelector } from '~/redux';
import { NameCard } from './NameCard';
import styles from './VotingPage.css';

function VotingPage() {
	const dispatch = useAppDispatch();
	const { getRemainingBabyNames, submitVote } = bindActionCreators(
		votingSlice.actions,
		dispatch
	);
	useEffect(() => {
		getRemainingBabyNames(NameGender.Male);
	}, []);
	const { currentName, isLoading } = useAppSelector((state) => state.voting);

	if (isLoading) {
		return <div>Loading...</div>;
	}

	if (currentName === null) {
		return <div>All done!</div>;
	}

	const createVoteClickHandler = (vote: Vote) => () => {
		submitVote({ id: currentName.id, vote: vote });
	};

	return (
		<>
			<div className={styles.container}>
				<NameCard name={currentName} />
				<div className={styles.buttons}>
					<button
						className={styles.nay}
						onClick={createVoteClickHandler(Vote.Nay)}
					>
						No
					</button>
					<button
						className={styles.yea}
						onClick={createVoteClickHandler(Vote.Yea)}
					>
						Yes
					</button>
				</div>
			</div>
		</>
	);
}

export default VotingPage;