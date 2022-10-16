import { useEffect, useState } from "react";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import LeagueTableTeam from '../LeagueComponents/LeagueTableTeam';
import Loading from '../Loading';
import { Link } from "react-router-dom";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { styled } from '@mui/material/styles';
import IconButton from '@mui/material/IconButton';

const ExpandMore = styled((props) => {
    const { expand, ...other } = props;
    return <IconButton {...other} />;
  })(({ theme, expand }) => ({
    transform: !expand ? 'rotate(0deg)' : 'rotate(180deg)',
    marginLeft: 'auto',
    transition: theme.transitions.create('transform', {
      duration: theme.transitions.duration.shortest,
    }),
  }));

const LeaugeStandings = (props) => {
    const [teams, setTeams] = useState({});
    const [loading, setLoading] = useState(true);
    const [expanded, setExpanded] = useState(false);

    const handleExpandClick = () => {
        setExpanded(!expanded);
    };

    useEffect(() => {
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Leagues/" + props.leagueId + "/Teams")
        .then((res) => setTeams(res.data))
        .then(() => setLoading(false))
    }, [props.leagueId])

    const tableDisplay = () => {
        if(expanded) {
            return <>
                {teams.map((t, i) =>
                    <LeagueTableTeam key={i} 
                        team={t}
                        position={i+1}
                    />
                )}
            </>
        }
        return <>
            {teams
            .slice(0, 5)
            .map((t, i) =>
                <LeagueTableTeam key={i} 
                    team={t}
                    position={i+1}
                />
            )}
        </>
    }

    return <>
        {loading && <Loading />}
        {!loading && <Table className='expandTable leagueTable tablesContainer container--styled'>
            <TableHead>
                <TableRow>
                    <TableCell>
                        <h3 className="tableHeaderTxt">
                            {props.title ? 
                            <Link style={{color: "black", textDecoration:"underline", fontSize: "0.9rem"}} to={"/leagues/"+props.leagueId}>{props.title} standings</Link> 
                            : "TEAMS"}
                        </h3>
                    </TableCell>
                    <TableCell align="center">
                        <h3 className="tableHeaderTxt">PL</h3>
                    </TableCell>
                    <TableCell className='tableColumnHide' align="center">
                        <h3 className="tableHeaderTxt">W</h3>
                    </TableCell>
                    <TableCell className='tableColumnHide' align="center">
                        <h3 className="tableHeaderTxt">D</h3>
                    </TableCell>
                    <TableCell className='tableColumnHide' align="center">
                        <h3 className="tableHeaderTxt">L</h3>
                    </TableCell>
                    <TableCell align="center">
                        <h3 className="tableHeaderTxt">GD</h3>
                    </TableCell>
                    <TableCell align="center">
                        <h3 className="tableHeaderTxt">PTS</h3>
                    </TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {!props.short ? teams.map((t, i) =>
                    <LeagueTableTeam key={i} 
                        team={t}
                        position={i+1}
                    />) : <>{tableDisplay()}</>   
                }
                {(teams.length > 4 && props.short) && <TableRow>
                <TableCell className='expandMoreRow' colSpan={7}>
                    <ExpandMore
                        expand={expanded}
                        onClick={handleExpandClick}
                        aria-expanded={expanded}
                        aria-label="show more"
                        >
                        <ExpandMoreIcon />
                    </ExpandMore>
                </TableCell>
            </TableRow>}
            </TableBody>
            </Table>}
    </>
}

export default LeaugeStandings;