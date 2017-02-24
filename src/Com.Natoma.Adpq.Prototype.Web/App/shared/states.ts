import { SelectItem } from 'primeng/primeng';

export class State {
    shortName: string;
    longName: string;
}

export class StatesFactory {
    static getStates(): State [] {
        return [
            { longName: "Alabama", shortName: "AL" },
            { longName: "Alaska", shortName: "AK" },
            { longName: "Arizona", shortName: "AZ" },
            { longName: "Arkansas", shortName: "AR" },
            { longName: "California", shortName: "CA" },
            { longName: "Colorado", shortName: "CO" },
            { longName: "Connecticut", shortName: "CT" },
            { longName: "Delaware", shortName: "DE" },
            { longName: "District of Columbia", shortName: "DC" },
            { longName: "Florida", shortName: "FL" },
            { longName: "Georgia", shortName: "GA" },
            { longName: "Hawaii", shortName: "HI" },
            { longName: "Idaho", shortName: "ID" },
            { longName: "Illinois", shortName: "IL" },
            { longName: "Indiana", shortName: "IN" },
            { longName: "Iowa", shortName: "IA" },
            { longName: "Kansas", shortName: "KS" },
            { longName: "Kentucky", shortName: "KY" },
            { longName: "Louisiana", shortName: "LA" },
            { longName: "Maine", shortName: "ME" },
            { longName: "Montana", shortName: "MT" },
            { longName: "Nebraska", shortName: "NE" },
            { longName: "Nevada", shortName: "NV" },
            { longName: "New Hampshire", shortName: "NH" },
            { longName: "New Jersey", shortName: "NJ" },
            { longName: "New Mexico", shortName: "NM" },
            { longName: "New York", shortName: "NY" },
            { longName: "North Carolina", shortName: "NC" },
            { longName: "North Dakota", shortName: "ND" },
            { longName: "Ohio", shortName: "OH" },
            { longName: "Oklahoma", shortName: "OK" },
            { longName: "Oregon", shortName: "OR" },
            { longName: "Maryland", shortName: "MD" },
            { longName: "Massachusetts", shortName: "MA" },
            { longName: "Michigan", shortName: "MI" },
            { longName: "Minnesota", shortName: "MN" },
            { longName: "Mississippi", shortName: "MS" },
            { longName: "Missouri", shortName: "MO" },
            { longName: "Pennsylvania", shortName: "PA" },
            { longName: "Rhode Island", shortName: "RI" },
            { longName: "South Carolina", shortName: "SC" },
            { longName: "South Dakota", shortName: "SD" },
            { longName: "Tennessee", shortName: "TN" },
            { longName: "Texas", shortName: "TX" },
            { longName: "Utah", shortName: "UT" },
            { longName: "Vermont", shortName: "VT" },
            { longName: "Virginia", shortName: "VA" },
            { longName: "Washington", shortName: "WA" },
            { longName: "West Virginia", shortName: "WV" },
            { longName: "Wisconsin", shortName: "WI" },
            { longName: "Wyoming", shortName: "WY" }
        ];
    }

    static getStatesAsSelectItems(): SelectItem[] {
        let items: SelectItem[] = [];
        for (let idx = 0; idx < StatesFactory.getStates().length; idx++) {
            items.push({ value: idx, label: StatesFactory.getStates()[idx].shortName });
        }
        return items;
    }
}