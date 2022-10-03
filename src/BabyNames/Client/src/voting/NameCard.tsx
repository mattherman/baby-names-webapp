import React from 'react';
import { IBabyName } from '~/models';
import './NameCard.css';

export interface INameCardProps {
	name: IBabyName;
}

export function NameCard({ name }: INameCardProps) {
	return <div className="root blue">{name.name}</div>;
}
