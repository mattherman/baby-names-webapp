import { createSlice } from '@reduxjs/toolkit';
import { IBabyName, IComparisonResult } from '~/models';
import { compareResults, getCompletedBabyNames } from './results.actions';

export interface IResultsState {
	isLoading: boolean;
	hasError: boolean;
	completedNames: IBabyName[];
	compareResult: IComparisonResult;
}

const initialState: IResultsState = {
	isLoading: false,
	hasError: false,
	completedNames: [],
	compareResult: null,
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
			})

			.addCase(compareResults.pending, (state) => {
				state.isLoading = true;
				state.hasError = false;
				state.compareResult = null;
			})
			.addCase(compareResults.fulfilled, (state, action) => {
				state.isLoading = false;
				state.compareResult = action.payload;
			})
			.addCase(compareResults.rejected, (state) => {
				state.isLoading = false;
				state.hasError = true;
			}),
});

export default {
	...slice,
	actions: {
		...slice.actions,
		getCompletedBabyNames,
		compareResults,
	},
};
