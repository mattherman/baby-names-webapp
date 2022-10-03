import { createAsyncThunk } from '@reduxjs/toolkit';
import * as hub from './voting.hub';
import { NameGender, VoteRequest } from '~/models';

export const getRemainingBabyNames = createAsyncThunk(
	'getRemainingBabyNames',
	async (gender?: NameGender) => {
		return await hub.getBabyNames(gender);
	}
);

export const submitVote = createAsyncThunk(
	'vote',
	async (voteRequest: VoteRequest) => {
		return await hub.submitVote(voteRequest);
	}
);
