const PlayerHeader = (props) => {
    return <div className="playerHeaderInfo">
        <img 
            className="playerImg" 
            src={props.player?.playerPerson?.image} 
            alt="Player"
        />
        <div className="playerName">
            <h2>{props.player?.playerPerson?.name}</h2>
            <p>{props.player?.position}</p>
        </div>
    </div>
}

export default PlayerHeader;

