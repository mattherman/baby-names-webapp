import React from 'react';
import { IBabyName } from '~/models';

interface IResultRowProps {
	babyName: IBabyName;
}

function ResultRow({ babyName }: IResultRowProps) {
	return <li>{babyName.name}</li>;
}

export default ResultRow;
