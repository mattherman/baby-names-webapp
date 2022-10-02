import { createAsyncThunk } from '@reduxjs/toolkit';
import * as hub from './names.hub';
import { NameGender } from '~/models';

export const getRemainingBabyNames = createAsyncThunk(
	'getRemainingBabyNames',
	async (gender?: NameGender) => {
		return await hub.getBabyNames(gender);
	}
);
