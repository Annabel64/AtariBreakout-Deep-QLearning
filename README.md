# Atari BreakOut with Deep QLearning
mission carried out withing Bandai Namco Studios Japan

## What is QLearning and Deep QLearning
Reinforcement learning is a machine learning method whose objective is to allow an agent (virtual entity: robot, program, etc.), placed in an interactive environment (its actions modify the state of the environment), to choose actions that maximize quantitative rewards. The agent experiments and improves its action strategy based on the rewards provided by the environment.

Deep qLearning is a Qlearning algorithm with a deep neural network added. This neural network helps speeding up the learning phase and choosing the best action for more complex states.

<img src="https://github-production-user-asset-6210df.s3.amazonaws.com/76532104/274910523-45b078a8-64d6-4dc9-af06-b6b4a1025854.png" width="1000" height="500" />

## Atari breackout
There are different possibilities to code this ANN, my choices were:

- The ANN takes the state of the interface as input (x_platform - x_ball, y_ball, vx_ball, vy_ball and action), and it gives me the "reward" of the action. When I want it to play, I will have to predict 3 values, associated with the left, right and motionless actions. I give in input the difference (x_platform-x_ball) instead of only x_ball and x_platform, so that the ANN see quicker that this variable has to tend to 0 to have a big reward.
- Initialize the weights with a formula and not randomly, I chose to use the formula of He-et-al: w = np.random.randn ( layer_size[l], layer_size[l-1] ) * np.sqrt( 1 / layer_size[l-1] )
Bellman's equation doesn’t intervene anymore, since only the reward interests us and the ANN predicts it.

As my ANN is working, I could easily go to the next steps:
- adding some bricks,
- adding another platform on the roof to make the two platforms "fight". For this, I decided to use only one neural network and to call it in mirror, as the two situations are identical but symmetric. First, so it learns 2 times faster, and then, to simplify the program and not have the same neural network twice. To make the platforms "fight", I give them the position of the other platform and a big reward when the ball falls on the other side.
