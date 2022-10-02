import { createSlice } from '@reduxjs/toolkit';
import { getRemainingBabyNames } from './names.actions';
import { IBabyName } from '~/models';

export interface IBabyNamesState {
	isLoading: boolean;
	hasError: boolean;
	remainingNames: IBabyName[];
	currentName: IBabyName;
}

const initialState: IBabyNamesState = {
	isLoading: false,
	hasError: false,
	remainingNames: [],
	currentName: null,
};

const slice = createSlice({
	name: 'names',
	initialState,
	reducers: {},
	extraReducers: (builder) =>
		builder
			.addCase(getRemainingBabyNames.pending, (state) => {
				state.isLoading = true;
				state.hasError = false;
				state.remainingNames = [];
				state.currentName = null;
			})
			.addCase(getRemainingBabyNames.fulfilled, (state, action) => {
				state.isLoading = false;
				state.remainingNames = action.payload;
			})
			.addCase(getRemainingBabyNames.rejected, (state) => {
				state.isLoading = false;
				state.hasError = true;
			}),
});

export default {
	...slice,
	actions: {
		...slice.actions,
		getRemainingBabyNames,
	},
};
