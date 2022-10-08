import { createAsyncThunk } from '@reduxjs/toolkit';
import * as babyNamesApi from '~/api/babyNames';
import { NameGender, VoteRequest } from '~/models';

export const getRemainingBabyNames = createAsyncThunk(
	'getRemainingBabyNames',
	async (gender?: NameGender) => {
		return await babyNamesApi.getBabyNames(gender);
	}
);

export const submitVote = createAsyncThunk(
	'vote',
	async (voteRequest: VoteRequest) => {
		return await babyNamesApi.submitVote(voteRequest);
	}
);
