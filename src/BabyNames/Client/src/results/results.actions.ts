import { BabyNamesRequest } from '~/models';
import * as babyNamesApi from '~/api/babyNames';
import { createAsyncThunk } from '@reduxjs/toolkit';
import { IState } from '~/redux';

export const getCompletedBabyNames = createAsyncThunk(
	'getCompletedBabyNames',
	async (babyNamesRequest: BabyNamesRequest, { getState }) => {
		const {
			session: { token },
		} = getState() as IState;
		const names = await babyNamesApi.getBabyNames(
			token,
			babyNamesRequest.gender,
			true
		);
		return names.filter((name) => name.vote != null);
	}
);
