import TableRow from '@mui/material/TableRow';
import TableCell from '@mui/material/TableCell';
import { useNavigate } from 'react-router-dom';

const LeagueTableTeam = (props) => {
    const navigate = useNavigate();
    const team = props.team;
    
    return <TableRow hover onClick={() => navigate("/Teams/" + team.id)}>
        <TableCell>
            <div className='flex flex-ai-c'>
                <p className='position'>
                    {props.position}
                </p>
                
                <img className="tableTeamLogo" src={team.logo} alt="teamLogo"/>
                <h5>{team.name}</h5>
            </div>
        </TableCell>
        <TableCell align="center">
            <h5>{team.currentSeasonStats.gamesPlayed}</h5>
        </TableCell>
        <TableCell className='tableColumnHide' align="center">
            <h5>{team.currentSeasonStats.gamesWon}</h5>
        </TableCell>
        <TableCell className='tableColumnHide' align="center">
            <h5>{team.currentSeasonStats.gamesDrawn}</h5>
        </TableCell>
        <TableCell className='tableColumnHide' align="center">
            <h5>{team.currentSeasonStats.gamesLost}</h5>
        </TableCell>
        <TableCell align="center">
            <h5>{team.currentSeasonStats.goalsFor - team.currentSeasonStats.goalsAgainst}</h5>
        </TableCell>
        <TableCell align="center">
            <h5>{team.currentSeasonStats.points}</h5>
        </TableCell>
    </TableRow>
}

export default LeagueTableTeam;