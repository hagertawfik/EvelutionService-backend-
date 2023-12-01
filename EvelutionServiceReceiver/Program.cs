using EvelutionServiceReceiver.DataContextEvelution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

using EvelutionServiceReceiver.DTO;
using EvelutionServiceReceiver.BussinesLogic;
using EvelutionServiceReceiver.Repository_Implementations;
using EvelutionServiceReceiver.Data_Models;
using Newtonsoft.Json;

IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

var optionsBuilder = new DbContextOptionsBuilder<DataContextEvelution1>();
optionsBuilder.UseSqlServer(connectionString);
using var context = new DataContextEvelution1(optionsBuilder.Options);

//receiver
ConnectionFactory factory = new();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit Receiver Evaluation Service";
IConnection cnn = factory.CreateConnection();
IModel channel = cnn.CreateModel();

string exchangeName = "EvelutionExchange";
string routingKey = "Evelution-routing-key";
string queueName = "EvelutionQueue";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName, exchangeName, routingKey, null);
channel.BasicQos(0, 1, false);
var consumer = new EventingBasicConsumer(channel);
consumer.Received += async (sender, args) =>
{
    try
    {
        var body = args.Body.ToArray();
        string message = Encoding.UTF8.GetString(body);
        SubmitExamRequestDto submitRequestExamDto = JsonConvert.DeserializeObject<SubmitExamRequestDto>($"{message}");
        Console.WriteLine(message);
        var _submitexamrepo = new SubmitExamRepository(context);
        var _submitExamService = new SubmitExamService(_submitexamrepo);
        var grade = _submitExamService.SubmitExam(submitRequestExamDto);
        Console.WriteLine(grade);
        var ExamResult = new ExamResult()
        {
            ExamId = submitRequestExamDto.ExamId,
            UserId = submitRequestExamDto.StudentId.ToString(),
            endTime = submitRequestExamDto.EndDateTime,
            Grade = grade,
            startTime = submitRequestExamDto.StartDateTime

        };
        var _examResultEvelutionRepository = new ExamResultEvelutionRepository(context);
        _examResultEvelutionRepository.AddExamResult(ExamResult);
        

        channel.BasicAck(args.DeliveryTag, false);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex.Message}");
    }


};
string consumerTag = channel.BasicConsume(queueName, false, consumer);
channel.BasicCancel(consumerTag);
channel.Close();
cnn.Close();
