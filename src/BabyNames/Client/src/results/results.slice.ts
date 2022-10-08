import { createSlice } from '@reduxjs/toolkit';
import { IBabyName } from '~/models';
import { getCompletedBabyNames } from './results.actions';

export interface IResultsState {
	isLoading: boolean;
	hasError: boolean;
	completedNames: IBabyName[];
}

const initialState: IResultsState = {
	isLoading: false,
	hasError: false,
	completedNames: [],
};

const slice = createSlice({
	name: 'results',
	initialState,
	reducers: {},
	extraReducers: (builder) =>
		builder
			.addCase(getCompletedBabyNames.pending, (state) => {
				state.isLoading = true;
				state.hasError = false;
				state.completedNames = [];
			})
			.addCase(getCompletedBabyNames.fulfilled, (state, action) => {
				state.isLoading = false;
				state.completedNames = action.payload;
			})
			.addCase(getCompletedBabyNames.rejected, (state) => {
				state.isLoading = false;
				state.hasError = true;
			}),
});

export default {
	...slice,
	actions: {
		...slice.actions,
		getCompletedBabyNames,
	},
};
