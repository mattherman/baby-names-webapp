import React, { useEffect } from 'react';
import { bindActionCreators } from 'redux';
import votingSlice from './voting.slice';
import { NameGender, Vote } from '~/models';
import { useAppDispatch, useAppSelector } from '~/redux';
import { NameCard } from './NameCard';
import styles from './VotingPage.css';
import Button from '~/components/Button';
import LoadingSpinner from '~/components/LoadingSpinner';
import Layout from '~/Layout';

export interface IVotingPageProps {
	gender: NameGender;
}

function VotingPage({ gender }: IVotingPageProps) {
	const dispatch = useAppDispatch();
	const { getRemainingBabyNames, submitVote } = bindActionCreators(
		votingSlice.actions,
		dispatch
	);
	useEffect(() => {
		getRemainingBabyNames({ gender });
	}, []);

	const currentName = useAppSelector((state) => state.voting.currentName);
	const isLoading = useAppSelector((state) => state.voting.isLoading);

	const createVoteClickHandler = (vote: Vote) => () => {
		submitVote({ id: currentName.id, vote: vote });
	};

	return (
		<Layout centered>
			{isLoading ? (
				<LoadingSpinner />
			) : currentName === null ? (
				<div className={styles.done}>All done!</div>
			) : (
				<div className={styles.root}>
					<NameCard name={currentName} />
					<div className={styles.buttons}>
						<Button onClick={createVoteClickHandler(Vote.Nay)}>No</Button>
						<Button onClick={createVoteClickHandler(Vote.Yea)}>Yes</Button>
					</div>
				</div>
			)}
		</Layout>
	);
}

export default VotingPage;
