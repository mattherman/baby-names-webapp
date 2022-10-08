import { NameGender } from '~/models';
import * as babyNamesApi from '~/api/babyNames';
import { createAsyncThunk } from '@reduxjs/toolkit';

export const getCompletedBabyNames = createAsyncThunk(
	'getCompletedBabyNames',
	async (gender?: NameGender) => {
		const names = await babyNamesApi.getBabyNames(gender, true);
		return names.filter((name) => name.vote != null);
	}
);
