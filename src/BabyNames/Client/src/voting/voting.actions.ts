import { createAsyncThunk } from '@reduxjs/toolkit';
import * as babyNamesApi from '~/api/babyNames';
import { BabyNamesRequest, VoteRequest } from '~/models';
import { IState } from '~/redux';

export const getRemainingBabyNames = createAsyncThunk(
	'getRemainingBabyNames',
	async (babyNamesRequest: BabyNamesRequest, { getState }) => {
		const {
			session: { token },
		} = getState() as IState;
		return await babyNamesApi.getBabyNames(token, babyNamesRequest.gender);
	}
);

export const submitVote = createAsyncThunk(
	'vote',
	async (voteRequest: VoteRequest, { getState }) => {
		const {
			session: { token },
		} = getState() as IState;
		return await babyNamesApi.submitVote(token, voteRequest);
	}
);
