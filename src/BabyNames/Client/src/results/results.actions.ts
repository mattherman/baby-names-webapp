import { BabyNamesRequest, IComparisonRequest } from '~/models';
import * as babyNamesApi from '~/api/babyNames';
import { createAsyncThunk } from '@reduxjs/toolkit';
import { IState } from '~/redux';

export const getCompletedBabyNames = createAsyncThunk(
	'getCompletedBabyNames',
	async (request: BabyNamesRequest, { getState }) => {
		const {
			session: { token },
		} = getState() as IState;
		const names = await babyNamesApi.getBabyNames(token, request.gender, true);
		return names.filter((name) => name.vote != null);
	}
);

export const compareResults = createAsyncThunk(
	'compareResults',
	async (request: IComparisonRequest, { getState }) => {
		const {
			session: { token },
		} = getState() as IState;
		return await babyNamesApi.compareResults(token, request);
	}
);
