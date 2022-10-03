import TableCell from '@mui/material/TableCell';

const CountryTableItem = (props) => {
    const displayFlag = () => {
        var country = props.country.toLowerCase();
        if(country.includes("republic")){
            country = country.replace("republic", "")
            country = country.replace("of", "")
            country = country.replace(/\s/g, "")
        }

        if(country.includes("korea")){
            country = 'kor';
        }

        if(country.includes("czech")){
            country = 'czechia';
        }

        if(country.includes("congo")){
            country = 'cod';
        }

        if(country.includes("dominican")){
            country = 'do';
        }

        if(country.includes("cape verde")){
            country = 'cv';
        }

        if(country.includes("russia")){
            country = 'ru'
        }

        if(country.includes("yugoslavia")){
            return <TableCell  align="center">
            <img 
                className="countryImg" 
                src="https://upload.wikimedia.org/wikipedia/commons/thumb/6/61/Flag_of_Yugoslavia_%281946-1992%29.svg/800px-Flag_of_Yugoslavia_%281946-1992%29.svg.png?20220813004708"
                alt={props.country}
            />
        </TableCell>
        }

        return <TableCell  align="center">
            <img className="countryImg" src={'https://countryflagsapi.com/png/'+ country} alt={props.country}/>
        </TableCell>
    }
    return <>{displayFlag()}</>
}

export default CountryTableItem;